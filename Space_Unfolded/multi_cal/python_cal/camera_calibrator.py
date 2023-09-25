import cv2
import numpy as np
import os
import json
import random
import glob

class CameraCalibrator:

    def __init__(self, folder, checkerboard_size=(7, 7), inner_corners=(6, 6)):
        self.folder = folder
        if not os.path.exists(self.folder):
            os.makedirs(self.folder)
        self.checkerboard_size = checkerboard_size
        self.inner_corners = inner_corners 
        self.objpoints = []
        self.imgpoints = []
        self.camera_matrix = None
        self.dist_coeffs = None

    def generate_and_show_checkerboard(self, resolution=(1920, 1080), display_id=0, square_size=100):
        """
        This method generates a checkerboard pattern and displays it on the screen.
        The checkerboard can be rotated and translated randomly.
        """
        
        #Experimental automated camera calibration on projected checkerboard. Poor performance.

        cv2.namedWindow("Translated Checkerboard", cv2.WND_PROP_FULLSCREEN)
        cv2.moveWindow("Translated Checkerboard", display_id * resolution[0], 0)
        cv2.setWindowProperty("Translated Checkerboard", cv2.WND_PROP_FULLSCREEN, cv2.WINDOW_FULLSCREEN)

        rows, cols = self.checkerboard_size
        checkerboard = np.zeros((rows * square_size, cols * square_size), dtype=np.uint8)
        for i in range(rows):
            for j in range(cols):
                if (i + j) % 2 == 0:
                    checkerboard[i * square_size:(i + 1) * square_size, j * square_size:(j + 1) * square_size] = 255

        # Calculate the size of the new bounding rectangle after rotation
        angle = random.uniform(0, 360)
        (h, w) = checkerboard.shape[:2]
        (cX, cY) = (w // 2, h // 2)
        M = cv2.getRotationMatrix2D((cX, cY), angle, 1.0)
        cos = np.abs(M[0, 0])
        sin = np.abs(M[0, 1])
        nW = int((h * sin) + (w * cos))
        nH = int((h * cos) + (w * sin))
        M[0, 2] += (nW / 2) - cX
        M[1, 2] += (nH / 2) - cY

        rotated_checkerboard = cv2.warpAffine(checkerboard, M, (nW, nH))

        # Add translation
        max_x = resolution[0] - rotated_checkerboard.shape[1]
        max_y = resolution[1] - rotated_checkerboard.shape[0]
        pos_x = random.randint(0, max_x)
        pos_y = random.randint(0, max_y)

        # Create background and place the rotated checkerboard
        background = np.zeros((resolution[1], resolution[0]), dtype=np.uint8)
        background[pos_y:pos_y+rotated_checkerboard.shape[0], pos_x:pos_x+rotated_checkerboard.shape[1]] = rotated_checkerboard

        cv2.imshow('Translated Checkerboard', background)
        cv2.waitKey(500)

    def capture(self, cap):
        """
        Capture images from the camera preview. 
        User needs to press 's' to save an image. Aim is to capture 10 images of the checkerboard pattern.
        """
        
        """
        #Experimental automated camera calibration on projected checkerboard. Poor performance.
        capture_device.set(cv2.CAP_PROP_AUTO_EXPOSURE, 0.25)  # 0.25 turns OFF camera auto exposure
        capture_device.set(cv2.CAP_PROP_EXPOSURE, -4)  # Set exposure to desired value

        display_id = int(input("Enter the display ID to show the checkerboard: "))
        num_images_required = 10
        num_images_captured = 0

        while num_images_captured < num_images_required:
            self.generate_and_show_checkerboard(resolution, display_id)
            
            ret, frame = capture_device.read()
            
            if ret:
                cv2.imwrite(os.path.join(self.folder, f"frame_{num_images_captured}.png"), frame)
                
                print(f"Captured {num_images_captured+1} of {num_images_required}")
                num_images_captured += 1
            else:
                print("Failed to grab frame")

        cv2.destroyAllWindows()

        """
        
        print("Please click on the camera preview then press 's' to capture an image. Please capture 10 images of the checkerboard pattern provided.")

        num = 0

        while num < 10:

            _, img = cap.read()

            k = cv2.waitKey(5)
            if k == 27:
                break
            elif k == ord('s'): # wait for 's' key to save and exit
                cv2.imwrite('camera_calibration_data/frame_' + str(num) + '.png', img)
                print("image saved!")
                num += 1

            cv2.imshow('Img',img)

        # Release and destroy all windows before termination
        cap.release()

        cv2.destroyAllWindows()
        
    def find_corners(self):
        """
        Find and refine checkerboard corners in the saved images.
        """
         
        objp = np.zeros((self.inner_corners[0] * self.inner_corners[1], 3), np.float32)
        objp[:, :2] = np.mgrid[0:self.inner_corners[0], 0:self.inner_corners[1]].T.reshape(-1, 2)

        size_of_chessboard_squares_mm = 20
        objp = objp * size_of_chessboard_squares_mm

        criteria = (cv2.TERM_CRITERIA_EPS + cv2.TERM_CRITERIA_MAX_ITER, 30, 0.001)
        self.objpoints = []
        self.imgpoints = []
        
        images = glob.glob('camera_calibration_data/*.png')
        
        for fname in images:
            print(f"Reading image from {fname} for corner detection.")
            img = cv2.imread(fname)
            gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
            ret, corners = cv2.findChessboardCorners(gray, self.inner_corners, None)
            
            if ret == True:
                print(f"Corners found in image {fname}.")
                self.objpoints.append(objp)
                corners2 = cv2.cornerSubPix(gray, corners, (11,11), (-1,-1), criteria)
                self.imgpoints.append(corners2)
                
                cv2.drawChessboardCorners(img, self.inner_corners, corners2, ret)
                cv2.imshow('Find Corners', img)
                cv2.waitKey(1000)
            else:
                print(f"Corners not found in image {fname}.")
                
        cv2.destroyAllWindows()


    def calibrate(self):
        """
        Calibrate the camera using the object points and image points.
        Compute the reprojection error to evaluate the quality of calibration.
        """
        
        ret, self.camera_matrix, self.dist_coeffs, rvecs, tvecs = cv2.calibrateCamera(self.objpoints, self.imgpoints, (1280, 720), None, None)
        if ret:
            print("Calibration successful!")
        else:
            print("Calibration failed.")

        mean_error = 0

        for i in range(len(self.objpoints)):
            imgpoints2, _ = cv2.projectPoints(self.objpoints[i], rvecs[i], tvecs[i], self.camera_matrix, self.dist_coeffs)
            error = cv2.norm(self.imgpoints[i], imgpoints2, cv2.NORM_L2)/len(imgpoints2)
            mean_error += error

        print( "total error: {}".format(mean_error/len(self.objpoints)) )

    def save_calibration(self, filename):
        """
        Save the calibration results (camera matrix and distortion coefficients) to a JSON file.
        """
        
        calibration_data = {
            "camera_matrix": self.camera_matrix.tolist(),
            "dist_coeffs": self.dist_coeffs.tolist()
        }
        with open(filename, 'w') as f:
            json.dump(calibration_data, f)

    def load_calibration(self, filename):
        """
        Load the calibration results from a JSON file.
        """
        
        with open(filename, 'r') as f:
            calibration_data = json.load(f)
        self.camera_matrix = np.array(calibration_data['camera_matrix'])
        self.dist_coeffs = np.array(calibration_data['dist_coeffs'])
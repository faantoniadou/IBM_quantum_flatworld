import os
import subprocess
import cv2 as cv
from threaded_video_capture import ThreadedVideoCapture
import open3d as o3d
import json



class MidasPCD:
    def __init__(self, camera_id=1):
        """Initialize the MidasPCD class with a given camera ID and default directories."""
        
        self.camera_id = camera_id
        self.camera = None
        self.script_directory = os.path.dirname(os.path.abspath(__file__))
        self.current_directory = os.path.dirname(self.script_directory)
        self.parent_directory = os.path.dirname(self.current_directory)
        self.midas_directory = os.path.join(os.path.dirname(self.parent_directory), "midas")
        self.input_directory = os.path.join(self.midas_directory, 'input')
        self.output_directory = os.path.join(self.midas_directory, 'output')
        self.camera_calibration_path = os.path.join(os.path.dirname(self.parent_directory), "camera_calibration.json")
        self._prepare_directories()

    def _prepare_directories(self):
        """Ensure the input and output directories exist and are empty."""  
        
        if not os.path.exists(self.input_directory):
            os.makedirs(self.input_directory)
        self._empty_directory(self.input_directory)
        self._empty_directory(self.output_directory)

    @staticmethod
    def _empty_directory(directory):
        """Empty a given directory by removing all its files."""    
        
        for filename in os.listdir(directory):
            file_path = os.path.join(directory, filename)
            try:
                if os.path.isfile(file_path):
                    os.unlink(file_path)
            except Exception as e:
                print(f'Failed to delete {file_path}. Reason: {e}')

    def open_camera(self):
        """Open the camera if it's not already open."""
    
        if not self.camera:
            self.camera = ThreadedVideoCapture(self.camera_id)

    def capture_frame_with_preview(self):
        """Capture a frame from the camera with a preview window. Allows user to press 's' to save the frame."""
        
        self.open_camera()
        captured_frame = None
        cv.namedWindow("Camera Preview - Press s to take the photo", cv.WINDOW_NORMAL)
        while True:
            frame = self.camera.read()
            cv.imshow("Camera Preview - Press s to take the photo", frame)
            key = cv.waitKey(1) & 0xFF
            if key == ord('s'):
                captured_frame = frame
                break
            elif key == ord('q'):
                break
        cv.destroyAllWindows()
        return captured_frame

    def save_frame(self, frame):
        """Save the captured frame to the input directory."""    
        
        if frame is not None:
            self._empty_directory(self.input_directory)  # Ensure the input folder is empty before saving the capture
            cv.imwrite(os.path.join(self.input_directory, 'captured_image.jpg'), frame)

    def close_camera(self):
        """Close the camera if it's open."""   
        
        if self.camera:
            self.camera.close()
            self.camera = None

    def run_depth_estimation(self):
        """Run the depth estimation script using MiDaS."""
        
        os.chdir(self.midas_directory)
        subprocess.run(["python", "run.py", "--model_type", "dpt_beit_large_512", "--input_path", "input", "--output_path", "output"])

            
    def generate_point_cloud(self):
        """Generate a point cloud from the estimated depth and the captured color image."""
        
        print(self.camera_calibration_path)    
        # Setting the paths for the images
        color_image_path = os.path.join(self.input_directory, "captured_image.jpg")
        depth_image_path = os.path.join(self.output_directory, "captured_image-dpt_beit_large_512.png")

        # Reading the images
        color_raw = o3d.io.read_image(color_image_path)
        depth_raw = o3d.io.read_image(depth_image_path)
        
        # Creating RGBD image
        rgbd_image = o3d.geometry.RGBDImage.create_from_color_and_depth(color_raw, depth_raw)
        
        # Reading the JSON file
        with open(self.camera_calibration_path, 'r') as file:
            calibration_data = json.load(file)

        camera_intrinsic = calibration_data['camera_matrix']

        # Set the intrinsic camera parameters
        camera_intrinsic_o3d = o3d.camera.PinholeCameraIntrinsic(
            width=1280, height=720, 
            fx=camera_intrinsic[0][0], 
            fy=camera_intrinsic[1][1], 
            cx=camera_intrinsic[0][2], 
            cy=camera_intrinsic[1][2]
        )
        print(camera_intrinsic_o3d.intrinsic_matrix)

        pcd = o3d.geometry.PointCloud.create_from_rgbd_image(rgbd_image, camera_intrinsic_o3d)
        pcd.transform([[1, 0, 0, 0], [0, -1, 0, 0], [0, 0, -1, 0], [0, 0, 0, 1]])
        o3d.visualization.draw_geometries([pcd])

    def estimate_depth(self):
        """Redefined method to capture a frame, save it, run the depth estimation, and then generate a point cloud."""
        
        frame = self.capture_frame_with_preview()
        self.save_frame(frame)
        self.close_camera()
        if frame is not None:
            self.run_depth_estimation()
            self.generate_point_cloud()
            
if __name__ == "__main__":
    estimator = MidasPCD()
    estimator.estimate_depth()

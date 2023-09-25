from calibration import Calibration
from display_manager import DisplayManager
from threaded_video_capture import ThreadedVideoCapture
from projector_manager import ProjectorManager
from camera_calibrator import CameraCalibrator
from midas_pcd import MidasPCD
from mss import mss
import numpy as np
import cv2
import json
import os

DATA_FOLDER = "data_test" 

QUIT_KEYS = {ord('q'), 27}

def save_maps_to_json(maps, filename):
    """Save calibration maps to a JSON file."""
    
    print("Saving calibration parameters, please wait.")
    maps_dict = {
        "data": [arr.tolist() for arr in maps],  # Convert numpy arrays to lists
        "dtype": [str(arr.dtype) for arr in maps]  # Save data type as string
    }
    with open(filename, 'w') as f:
        json.dump(maps_dict, f)

def load_maps_from_json(filename):
    """Load calibration maps from a JSON file."""
    
    with open(filename, 'r') as f:
        maps_dict = json.load(f)

    maps = [np.array(data, dtype=np.dtype(dtype)) for data, dtype in zip(maps_dict['data'], maps_dict['dtype'])]
    return maps

def proj_cal(num):
    """Calibrate the projector(s) and save the calibration maps."""
    
    dm = DisplayManager()
    calib = Calibration(DATA_FOLDER)
    maps = []
    for i in range(num):
        tvc = ThreadedVideoCapture(1)
        projector = dm.available_monitors[i+1]
        calib.capture((1920, 1080), tvc, projector)
        calib.calibrate()
        map_i, map_i_plus_1 = calib.read_maps()
        maps.extend([map_i, map_i_plus_1])
        tvc.close()
        
    save_maps_to_json(maps, 'calibration_maps.json')
    return maps

def display(num_projectors):
    """Mirror the screen to projector(s) using the calibration maps."""
    
    if os.path.exists('calibration_maps.json'):
        maps = load_maps_from_json('calibration_maps.json')
        manager = ProjectorManager(num_projectors)
        manager.initialize(maps)
        display_loop(manager)
    else:
        print("Calibration maps not found. Running calibration...")
        maps = proj_cal(num_projectors)
        manager = ProjectorManager(num_projectors)
        manager.initialize(maps)
        display_loop(manager)

def calibrate_camera():
    """Calibrate the camera and save the intrinsic matrix and distortion coefficients."""

    # Initialize OpenCV VideoCapture
    cap = cv2.VideoCapture(1, cv2.CAP_DSHOW)  # Assuming camera index is 1
    cap.set(cv2.CAP_PROP_FRAME_WIDTH, 1280)
    cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 720)
    # Initialize CameraCalibrator
    camera_cal = CameraCalibrator("camera_calibration_data")
    # Capture checkerboard images
    camera_cal.capture(cap)
    # Find corners
    camera_cal.find_corners()
    # Calibrate camera
    camera_cal.calibrate()
    # Save calibration data
    camera_cal.save_calibration('camera_calibration.json')
    # Release the camera
    cap.release()

    print("Camera calibration complete")

    main()

def point_cloud():
    """Generate a point cloud using Intel MiDaS monocular depth estimation *Requires camera to be calibrated first*."""
    
    estimator = MidasPCD()
    
    if not os.path.exists(estimator.camera_calibration_path):
        print("Camera calibration file not found. Please calibrate the camera first.")
        main()
        return
    
    estimator.estimate_depth()
    main()

def main():
    """Main menu for user to select options."""

    print("""Welcome to the IBM Quantum Flatworld Projection System: \n
    [1] Calibrate Projector(s)
    [2] Mirror Screen to projector(s)
    [3] Calibrate Camera
    [4] Generate Point cloud
    [5] Exit
          """)

    selection = input("\nPlease select an option: ")

    if selection == "1":
        while True:
            num_projectors = int(input("Enter the number of projectors (up to 3): "))
            if 1 <= num_projectors <= 3:
                maps = proj_cal(num_projectors)
                
                print("Calibration complete\n")
                main()
            else:
                print("Invalid number of projectors. Please enter a number between 1 and 3.")
        
    elif selection == "2":
        while True:
            num_projectors = int(input("Enter the number of projectors (up to 3): "))
            if 1 <= num_projectors <= 3:
                display(num_projectors)
            else:
                print("Invalid number of projectors. Please enter a number between 1 and 3.")

    elif selection == "3":
        calibrate_camera()

    elif selection == "4":
        point_cloud()

    elif selection == "5":
        exit()

    else:
        print("Invalid option, please try again.")
        main()


def display_loop(manager):
    """Main loop to mirror the screen to the projector(s) using the calibration maps."""
    
    monitor_main = {'left': 0, 'top': 0, 'width': 1920, 'height': 1080} #mirroed monitor
    img_np = np.zeros((monitor_main['height'], monitor_main['width'], 4), dtype=np.uint8)
    img_rgb = np.zeros((monitor_main['height'], monitor_main['width'], 3), dtype=np.uint8)

    with mss() as sct:
        while True:
            screenShot = sct.grab(monitor_main)
            img_np = np.array(screenShot)
            img_rgb = img_np[..., :3]
            manager.update_all(img_rgb)
            if cv2.waitKey(33) & 0xFF in QUIT_KEYS:
                break

    cv2.destroyAllWindows()
    main()

if __name__ == "__main__":
    main()

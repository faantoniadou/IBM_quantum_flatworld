import itertools
import os
import cv2 as cv
from ctypes import *
from display_manager import Monitor
from threaded_video_capture import ThreadedVideoCapture

class Calibration:
    def __init__(self, data_folder):
        """Initialize the Calibration class with the given data folder and load the calibration DLL."""
        
        self.data_folder = data_folder
        if not os.path.exists(self.data_folder):
            os.makedirs(self.data_folder)
        self.script_dir = os.path.dirname(os.path.abspath(__file__))
        self.dll_path = os.path.join(self.script_dir, "dlls", "ProjectionCalibration.dll")
        self.calib_dll = cdll.LoadLibrary(self.dll_path)

    def capture(self, projection_size: tuple[int, int], camera: ThreadedVideoCapture, projector: Monitor):
        """Capture gray code patterns for calibration using the given camera and projector."""
        
        gcp = cv.structured_light.GrayCodePattern.create(*projection_size)
        captured_frames = []
        
        projector.open_fullscreen()
        black_projection, white_projection = gcp.getImagesForShadowMasks(projection_size, projection_size)
        
        projector.display_image(black_projection)
        for projection in itertools.chain(gcp.generate()[1], [black_projection, white_projection]):
            projector.display_image(projection)
            cv.waitKey(500)
            captured_frames.append(camera.read())

        projector.close()
        camera.close()

        fs = cv.FileStorage(os.path.join(self.data_folder, "projection_size.ext"), cv.FILE_STORAGE_WRITE)
        fs.write("h", projection_size[1])
        fs.write("w", projection_size[0])
        fs.release()

        filenames = [f"pattern{i}.png" for i in range(len(captured_frames[:-2]))]
        for fname, pattern_image in zip(filenames, captured_frames[:-2]):
            cv.imwrite(os.path.join(self.data_folder, fname), pattern_image)
        
        cv.imwrite(os.path.join(self.data_folder, "blackFrame.png"), captured_frames[-2])
        cv.imwrite(os.path.join(self.data_folder, "whiteFrame.png"), captured_frames[-1])

    def calibrate(self):
        """Perform the calibration using the captured patterns and save the result."""
        
        r = cv.selectROI("Select the display contour", cv.imread(os.path.join(self.data_folder, "whiteFrame.png")))
        cv.destroyWindow("Select the display contour")
        contour = (c_int * 8)(*(itertools.chain.from_iterable([(r[0], r[1]), (r[0] + r[2], r[1]), (r[0] + r[2], r[1] + r[3]), (r[0], r[1] + r[3])])))
        self.calib_dll.calibrate(self.data_folder.encode(), contour)

    def read_maps(self):
        """Read and return the generated calibration maps."""
        
        fs = cv.FileStorage(os.path.join(self.data_folder, "map.ext"), cv.FILE_STORAGE_READ)
        map1 = fs.getNode("map1").mat()
        map2 = fs.getNode("map2").mat()
        fs.release()
        return map1, map2

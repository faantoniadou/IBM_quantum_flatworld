import cv2
import numpy as np

class Projector:
    def __init__(self, monitor_settings, map1, map2):
        """Initialize the Projector class with monitor settings and calibration maps."""
        
        self.monitor_settings = monitor_settings
        self.map1 = map1
        self.map2 = map2
        self.result = np.zeros((monitor_settings['height'], monitor_settings['width'], 3), dtype=np.uint8)
        self.create_window()

    def create_window(self):
        """Create a named window for the projector display, set its size and properties."""
        
        cv2.namedWindow(self.monitor_settings['name'], cv2.WINDOW_NORMAL)
        cv2.moveWindow(self.monitor_settings['name'], self.monitor_settings['x'], self.monitor_settings['y'])
        cv2.resizeWindow(self.monitor_settings['name'], self.monitor_settings['width'], self.monitor_settings['height'])
        cv2.setWindowProperty(self.monitor_settings['name'], cv2.WND_PROP_FULLSCREEN, cv2.WINDOW_FULLSCREEN)

    def update(self, img_rgb_portion):
        """Update the projector display with the stretched portion of the image based on calibration maps."""
        
        # Stretch the image portion to fit the projector's width
        stretched_img = cv2.resize(img_rgb_portion, (self.monitor_settings['width'], self.monitor_settings['height']))
        
        cv2.remap(stretched_img, self.map1, self.map2, cv2.INTER_LINEAR, dst=self.result)
        cv2.imshow(self.monitor_settings['name'], self.result)


class ProjectorManager:
    def __init__(self, num_projectors):
        """Initialize the ProjectorManager class with the desired number of projectors."""
        
        self.projectors = []
        self.num_projectors = num_projectors

    def initialize(self, maps):
        """
        Initialize the projector displays based on the provided calibration maps.
        Check if the provided maps are sufficient for the number of desired projectors.
        """
        
        if len(maps) < 2 * self.num_projectors:
            print(f"Warning: Only {len(maps) // 2} calibration maps available for {self.num_projectors} projectors. Initializing only available projectors.")
            self.num_projectors = len(maps) // 2

        for i in range(self.num_projectors):
            monitor = {'x': 1920*(i+1), 'y': 0, 'width': 1920, 'height': 1080, 'name': f'mon_{i+1}'}
            projector = Projector(monitor, maps[2*i], maps[2*i+1])
            self.projectors.append(projector)

    def update_all(self, img_rgb):
        """Update all projector displays with the provided RGB image."""
        
        # Calculate the width of each portion based on the overlap requirement
        full_width = img_rgb.shape[1]
        portion_width = int(0.55 * full_width)

        # Left projector gets the first 65% of the image
        left_img = img_rgb[:, :portion_width]
        # Right projector gets the last 65% of the image
        right_img = img_rgb[:, -portion_width:]
        
        self.projectors[0].update(left_img)
        self.projectors[1].update(right_img)
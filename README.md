# Image-Processing-Library
Library for processing and manipulating images. Includes edge detection, color channel swapping, embossing, sharpening thresholding and more. Some of the filters are applied by using <a href="https://en.wikipedia.org/wiki/Kernel_(image_processing)">kernel convolution</a> and <a href="https://msdn.microsoft.com/en-us/library/system.drawing.bitmap.lockbits(v=vs.110).aspx">LockBits</a> while others use multithreading where applicable. Also includes a histogram generator and a hue detector.

# Samples

The documentation below exhibits what is possible with the library. The following image will be used for the demonstrations:

![alt-tag] (Samples/sample_image.jpg)

## Edge Detection

The library supports several different edge detection algorithms with different intensities.

### Low Edge Detection

![alt-tag] (Samples/edges_low.jpg)

### High Edge Detection

![alt-tag] (Samples/edge_high.jpg)

### Horizontal Edge Detection

![alt-tag] (Samples/edges_horizontal.jpg)

### Vertical Edge Detection

![alt-tag] (Samples/edges_vertical.jpg)

## Thresholding

The library supports three different thresholding algorithms. These algorithms use LockBits and multithreading and are processed much faster than the others; almost instantaneously.

### Binary Thresholding

![alt-tag] (Samples/threshold.jpg)

### Trinary Thresholding

![alt-tag] (Samples/threshold_2.jpg)

### Multi-Level Thresholding

![alt-tag] (Samples/threshold_3.jpg)

## Blurring

Different intensities of blurring are available, as well as motion blur.

### Regular Blur

![alt-tag] (Samples/blur_high.jpg)

### Motion Blur

![alt-tag] (Samples/motion_blur.jpg)

## Sharpening

![alt-tag] (Samples/sharpen.jpg)

## Embossing

![alt-tag] (Samples/emboss.jpg)

## Color Channel Manipulation

The library supports many different algorithms for manipulating the color channels, including swapping, removal and intensity adjustment:

### Channel Swapping

The color channels can be swapped in many different ways. The following image has its green and blue channels swapped:

![alt-tag] (Samples/SwapGreenAndBlue.jpg)

### Channel Removal

Each color channel can be removed. The following image has its red channel completely dialed down to zero:

![alt-tag] (Samples/RemoveRed.jpg)

### Intensity Adjustment

Each color channel can by dialed up or down to match the intensity of another color channel. The following image has its blue channel dialed up to match the intensity of its red channel, this creates purple where the red had high intensity:

![alt-tag] (Samples/BlueToRed.jpg)

## Brighten

![alt-tag] (Samples/brighten.jpg)

## Darken

![alt-tag] (Samples/darken.jpg)

## Invert

![alt-tag] (Samples/invert.jpg)

## Cartoonify

![alt-tag] (Samples/cartoonify.jpg)

## Histogram Generator

The following is a histogram of the RGB channels of the original image.

![alt-tag] (Samples/histogram.png)

## Hue Detector

The following detects the hue under the mouse and displays the details.

![alt-tag] (Samples/hue_detection.gif)

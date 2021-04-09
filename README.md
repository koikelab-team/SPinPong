# SPinPong - VR TableTennis Training System

This is the official repository of the paper "SPinPong - Virtual Reality Table Tennis Skill Acquisition using Visual, Haptic and Temporal Cues" which deals with a VR table tennis training system targetting novice players. The system is designed to make users understand and acquire the specific skill of returning strong spin serves. This approach makes use of slow-motion (which we call Temporal Distortion) as well as various visual cues as shown below.

![teaser1](https://github.com/koikelab-team/SPinPong/blob/master/fig/teaser1.GIF?raw=true)
<details>
  <summary>More</summary>

![teaser2](https://github.com/koikelab-team/SPinPong/blob/master/fig/teaser2.GIF?raw=true)
![teaser3](https://github.com/koikelab-team/SPinPong/blob/master/fig/teaser3.GIF?raw=true)
</details>

## Requirements
- Unity
- 1x SteamVR compatible HMD
- 1x Controller

### For greater immersion
- 1x VIVE Tracker attached to a racket
- audio recorder / vibrator + microcontroller for haptic feedback

## Usage
### Preparation
Place your VRHMD on the user's side of the table, as closely as possible to the edge without any overhang. See below:
```
 __R__  <- robot
|  |  |
|  |  |
|-----| <- net
|  |  |
|__H__| <- user side. H: Hmd
```
The HMD should face towards the robot. Now calibrate SteamVR. Height: 0.76cm (normal table height).

### Run
1. Now build & run the provided Unity application. Check if the headset is correctly calibrated. Adjust its position via the coordinates of the user GameObject.
2. You can run the different conditions via the keyboard:
- 1: VR condition without any additional cues. Slow-mo is enabled.
- 2: Bullet-Time. On top of cond. 1, a small window pops up showing the ball, while time is slowed down extremely.
- 3: Guidance: On top of cond. 1, trajectory of the ball is shown with a semi-transparent racket that shows how to swing the racket for a successful return.
- 4: Arrow: On top of cond. 1, green arrows rotate around the ball to indicate the rotation of the ball.
- Escape: Quit applicatoin.

## Citation

This is the official repository of the paper "SPinPong - Virtual Reality Table Tennis Skill Acquisition using Visual, Haptic and Temporal Cues" (IEEE VR Journal 2021).

If you refer to our paper or use our code, please also cite this paper and the previous work:
```
E. Wu, M. Piekenbrock, T. Nakumura and H. Koike, "SPinPong - Virtual Reality Table Tennis Skill Acquisition using Visual, Haptic and Temporal Cues," in IEEE Transactions on Visualization and Computer Graphics, doi: 10.1109/TVCG.2021.3067761.
```

## License
The code, software, and data in this repository is only available for non-commercial research use. Please see the [license](https://github.com/koikelab-team/SPinPong/blob/master/LICENSE) for further details.

# Buttons are not hittest visible in non-client area

## Requirements

1. Windows 10 20H2 Insiders Preview
  * 版本	Windows 10 专业版 Insider Preview
  * 版本号	2004
  * 安装日期	2020/10/9
  * 操作系统版本	20231.1005
  * 体验	Windows Feature Experience Pack 120.26102.0.0
2. A WPF window as the parent window
3. Any window as a child window (no matter that they are in the same thread / different threads / different processes)

## Reproduction

1. Launch the demo app
2. Hover and click the top-right corner button
3. Click the "embed" button
4. Hover and click the top-right corner button again
5. Click the "release" button
6. Hover and click the top-right corner button again

You'll see that if there is a child window inside, the top-right corner button does not respond to the mouse.

## Analysis

If we hook the parent window, we'll see that the parent window receives no messages while the mouse is moving over the non-client area. But is the child window is released, the `NCHITTEST` message comes back again.

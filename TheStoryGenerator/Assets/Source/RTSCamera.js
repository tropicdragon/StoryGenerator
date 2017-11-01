var CamSpeed = 1.00;
var GUIsize = 25;
 
function Update () {
var recdown = Rect (0, 0, Screen.width, GUIsize);
var recup = Rect (0, Screen.height-GUIsize, Screen.width, GUIsize);
var recleft = Rect (0, 0, GUIsize, Screen.height);
var recright = Rect (Screen.width-GUIsize, 0, GUIsize, Screen.height);
 
    if (recdown.Contains(Input.mousePosition))
        transform.Translate(0, 0, -CamSpeed, Space.World);
 
    if (recup.Contains(Input.mousePosition))
        transform.Translate(0, 0, CamSpeed, Space.World);
 
    if (recleft.Contains(Input.mousePosition))
        transform.Translate(-CamSpeed, 0, 0, Space.World);
 
    if (recright.Contains(Input.mousePosition))
        transform.Translate(CamSpeed, 0, 0, Space.World);

 }
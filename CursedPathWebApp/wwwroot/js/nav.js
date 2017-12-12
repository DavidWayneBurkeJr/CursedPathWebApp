/* Set the width of the side navigation to 250px and the left margin of the page content to 250px and add a black background color to body */
function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
    document.getElementById("main").style.marginLeft = "250px";
    document.getElementById("myNav").style.marginLeft = "250px"
    document.body.style.backgroundColor = "rgba(12,13,14,1.0)";
    document.getElementById("openButt").style.marginLeft = "-50px";

}

/* Set the width of the side navigation to 0 and the left margin of the page content to 0, and the background color of body to white */
function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
    document.getElementById("main").style.marginLeft = "0";
    document.getElementById("myNav").style.marginLeft = "0";
    document.body.style.backgroundColor = "#1C1A1A";
    document.getElementById("openButt").style.marginLeft = "0px";
} 
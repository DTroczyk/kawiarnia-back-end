function getBackground(name) {
  switch (name) {
    case "mocca":
      const moccaBg = {
        background:
          "url(https://img.wallpapersafari.com/desktop/1920/1080/45/90/dCS7mf.jpg) center",
      };
      return moccaBg;
    case "flatWhite":
      const flatwhiteBg = {
        background:
          "url(https://images.unsplash.com/photo-1459755486867-b55449bb39ff?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1949&q=80) center",
      };
      return flatwhiteBg;
    case "latte":
      const latteBg = {
        background:
          "url(https://images.unsplash.com/photo-1563090308-5a7889e40542?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=2134&q=80) center",
      };
      return latteBg;
    case "americana":
      const americanoBg = {
        background:
          "url(https://images.unsplash.com/photo-1521302080334-4bebac2763a6?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=2000&q=80) center",
      };
      return americanoBg;
    case "espresso":
      const espressoBg = {
        background:
          "url(https://cdn.hipwallpaper.com/i/93/85/9U1NnJ.jpg) center",
      };
      return espressoBg;
    default:
      break;
  }
}
export default getBackground;

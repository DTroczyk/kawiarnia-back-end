import React from "react";
import "./NewOrder.scss";
const moccaBg = {
  background: 'url(https://img.wallpapersafari.com/desktop/1920/1080/45/90/dCS7mf.jpg)',

};
function NewOrder() {
  return (
    <div className="newOrder">
      <div style={moccaBg} className="newOrder__coffee">Latte</div>
      <div style={moccaBg} className="newOrder__coffee">Mocca</div>
      <div style={moccaBg} className="newOrder__coffee">Americana</div>
      <div style={moccaBg} className="newOrder__coffee">Flat White</div>
    </div>
  );
}
export default NewOrder;

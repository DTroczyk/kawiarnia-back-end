import React, { useState, Fragment } from "react";
import { Map as LeafletMap, Marker, Popup, TileLayer } from "react-leaflet";
import { useSelector, useDispatch } from "react-redux";
import "./Map.scss";
import orderActions from "../../redux/order/actions";
export default function Map() {
  const dispatch = useDispatch();
  const [adress, setAdress] = useState("");
  const latLng = useSelector((state) => state.order.latLng);
  return (
    <Fragment>
      <span> Zaznacz miejsce dostawy lub wpisz adres </span>
      <input
        type="text"
        onChange={(e) => setAdress(e.target.value)}
        placeholder="Adres..."
      />

      <div className="leaflet-container">
        <LeafletMap
          center={[50.625481, 18.836307]}
          zoom={18}
          onClick={(event) =>
            dispatch(
              orderActions.setLatLng([event.latlng.lat, event.latlng.lng])
            )
          }
        >
          <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
          {latLng ? (
            <Marker position={latLng}>
              <Popup>
                Teraz już wiecie gdzie możecie przyjechać na kawe :D
              </Popup>
            </Marker>
          ) : null}
        </LeafletMap>
      </div>
      <button onClick={()=>{
        const pdf = document.querySelector('.newOrder__pdf')
        pdf.scrollIntoView({
          behavior:"smooth",
          block:"start",
          inline:"end"
        })
      }}>&#129047;</button>
    </Fragment>
  );
}

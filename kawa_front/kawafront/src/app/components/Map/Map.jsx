import React from "react";
import { Map as LeafletMap, Marker, Popup, TileLayer } from "react-leaflet";
import './Map.scss'
export default function Map() {

  return (
    <div className="leaflet-container">
      <LeafletMap center={[50.625481, 18.836307]} zoom={18} onClick={event=>console.log(event)}>
        <TileLayer
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
          attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
        />
      
        <Marker position={[50.62559413917391,  18.83555822215451]}>
            <Popup>
                Teraz już wiecie gdzie możecie przyjechać na kawe :D 
            </Popup>
        </Marker>
      </LeafletMap>
    </div>
  );
}

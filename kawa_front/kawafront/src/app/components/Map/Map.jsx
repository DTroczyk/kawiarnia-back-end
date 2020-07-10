import React,{useState} from "react";
import { Map as LeafletMap, Marker, Popup, TileLayer } from "react-leaflet";
import './Map.scss'
export default function Map() {
  const [latLng,setLatLng] = useState([50.62559413917391,  18.83555822215451])
  return (
    <div className="leaflet-container">
      <LeafletMap center={[50.625481, 18.836307]} zoom={18} onClick={event=>setLatLng([event.latlng.lat,event.latlng.lng])}>
        <TileLayer
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
          attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
        />
      
        <Marker position={latLng}>
            <Popup>
                Teraz już wiecie gdzie możecie przyjechać na kawe :D 
            </Popup>
        </Marker>
      </LeafletMap>
    </div>
  );
}

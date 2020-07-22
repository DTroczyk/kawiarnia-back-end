import React  from "react";
import { Map as LeafletMap, Marker, Popup, TileLayer } from "react-leaflet";
import { useSelector, useDispatch } from "react-redux";
import "./Map.scss";
import orderActions from "../../redux/order/actions";
import ScrollButton from "../ScrollButton/ScrollButton";
import { Wrapper, Input, Container } from "./components";
import useGeolocation from "../../hooks/useGeolocation";
export default function Map() {
  const dispatch = useDispatch();
  const location = useGeolocation();
  const latLng = useSelector((state) => state.order.latLng);

  return (
    <Wrapper>
      <Input />
      <Container>
        <LeafletMap
          center={latLng?latLng:[50.286263,19.104078]}
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
      </Container>
      {latLng ? <ScrollButton goTo="payment" /> : null}
    </Wrapper>
  );
}

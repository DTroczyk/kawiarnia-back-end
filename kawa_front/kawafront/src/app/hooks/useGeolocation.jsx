import React from "react";
import { useEffect, useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import orderActions from "../redux/order/actions";
function useGeolocation() {
  const dispatch = useDispatch();
  const location = useSelector((state) => state.order.latLng);
  useEffect(() => {
    navigator.geolocation.getCurrentPosition((pos) =>
      dispatch(
        orderActions.setLatLng([pos.coords.latitude, pos.coords.longitude])
      )
    );
  }, []);
}
export default useGeolocation;

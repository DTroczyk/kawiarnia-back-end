import React from "react";
import { useSelector } from "react-redux";

export default () => {
  const token = useSelector((state) => state.user.token);
  return token;
};

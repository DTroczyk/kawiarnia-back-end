import React from "react";
import { getCoffeeBackground } from "../../../helpers";
const Field = ({ children, coffeeName }) => (
  <div style={getCoffeeBackground(coffeeName)} className="bucket__field">
    {children}
  </div>
);
export default Field;

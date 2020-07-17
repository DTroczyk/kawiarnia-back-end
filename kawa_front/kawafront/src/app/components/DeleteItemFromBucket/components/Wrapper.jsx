import React from "react";
import { getCoffeeBackground } from "../../../helpers";
const Wrapper = ({ children, name }) => (
  <div style={getCoffeeBackground(name)} className="deleteItemFromBucket">
    {children}
  </div>
);
export default Wrapper;

import React, { useState } from "react";

function Input() {
  const [adress, setAdress] = useState("");
  return (
    <input
      type="text"
      onChange={(e) => setAdress(e.target.value)}
      placeholder="Adres..."
      value={adress}
    />
  );
}
export default Input;

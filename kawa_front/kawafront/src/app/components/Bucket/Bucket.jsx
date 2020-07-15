import React, { useState } from "react";
import "./Bucket.scss";
import { useDispatch, useSelector } from "react-redux";
import bucketActions from "../../redux/bucket/actions";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
const moccaBg = {
  background:
    "url(https://img.wallpapersafari.com/desktop/1920/1080/45/90/dCS7mf.jpg) center",
};
const latteBg = {
  background:
    "url(https://images.unsplash.com/photo-1563090308-5a7889e40542?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=2134&q=80) center",
};
const americanoBg = {
  background:
    "url(https://images.unsplash.com/photo-1521302080334-4bebac2763a6?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=2000&q=80) center",
};
const flatwhiteBg = {
  background:
    "url(https://images.unsplash.com/photo-1459755486867-b55449bb39ff?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1949&q=80) center",
};
function getTotalValue(itemsArr) {
  let acc = 0;
  itemsArr.map((item) => (acc += item.price));
  return Math.round(acc * Math.pow(10, 2)) / Math.pow(10, 2);
}

function Bucket() {
  const bucketItems = useSelector((state) => state.bucket.bucketItems);
  const dispatch = useDispatch();
  const [selectedItem, setSelectedItem] = useState(null);
  function handleDelete(idx) {
    

    toast.warn("Usunięto z koszyka", {
      position: "top-left",
      autoClose: 5000,
      hideProgressBar: true,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
    });
    dispatch(bucketActions.deleteItemFromBucket(idx));

  }
  return (
    <div className="bucket">
      <div className="bucket__info">
        <p className="bucket__text">
          Wartość Twojego koszyka: {getTotalValue(bucketItems)}zł
        </p>
        <p>{selectedItem?.coffeeName}</p>
      </div>
      <div className="bucket__block">
        {bucketItems.length
          ? bucketItems.map((item, idx) => (
              <div
                onClick={() => setSelectedItem(item)}
                key={idx}
                style={moccaBg}
                className="bucket__field"
              >
                <span className="bucket__name">{item.coffeeName}</span>
                <button className="bucket__button">Opłać zamówienie</button>
                <button
                  className="bucket__button"
                  onClick={() => handleDelete(idx)}
                >
                  &#128465;
                </button>
              </div>
            ))
          : "Brak zamowień"}
      </div>
      <ToastContainer
        position="top-left"
        autoClose={5000}
        hideProgressBar
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
      />
    </div>
  );
}
export default Bucket;

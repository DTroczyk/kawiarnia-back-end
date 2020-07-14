import React from "react";
import "./Bucket.scss";
import { useDispatch, useSelector } from "react-redux";
import bucketActions from "../../redux/bucket/actions";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
function Bucket() {
  const bucketItems = useSelector((state) => state.bucket.bucketItems);
  const dispatch = useDispatch();
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
      {bucketItems.map((item, idx) => (
        <>
          <span>{item.coffeeName}</span>
          <button onClick={() => handleDelete(idx)}> &#128465;</button>
        </>
      ))}
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

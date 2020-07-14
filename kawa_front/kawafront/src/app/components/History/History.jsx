import React, { Fragment } from "react";
import "./History.scss";
import { useSelector } from "react-redux";
function History() {
  const historyData = useSelector(state=>state.history[0])
  
  function handleClick(idx) {
    console.log(idx);
  }
  return (
    <div className="history">
      {historyData.map((order, idx) => {
        const { date, name, count, price, status, isCollapsed } = order;
        return (
          <Fragment key={idx}>
            <div className="history__field" onClick={() => handleClick(idx)}>
              <div className="history__block">{date}</div>
              <div className="history__block">{name}</div>
              <div className="history__block">{count}</div>
              <div className="history__block">{price}</div>
              <div className="history__block">{status}</div>
            </div>
            {isCollapsed && <p>ELO</p>}
          </Fragment>
        );
      })}
    </div>
  );
}
export default History;

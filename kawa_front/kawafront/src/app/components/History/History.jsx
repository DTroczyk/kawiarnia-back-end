import React, { Fragment } from "react";
import "./History.scss";
import { useSelector, useDispatch } from "react-redux";
import historyActions from "../../redux/history/actions";
import Details from "../Details/Details";
import FadeIn from "react-fade-in";
function History() {
  const historyItems = useSelector((state) => state.history.historyItems);
  const dispatch = useDispatch();
  function handleClick(idx) {
    dispatch(historyActions.toggleOrderDetailsVisible(idx));
  }
  return (
    <div className="history">
      {historyItems.map((order, idx) => {
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
            {isCollapsed && (
              <FadeIn>
                <Details idx={idx} />
              </FadeIn>
            )}
          </Fragment>
        );
      })}
    </div>
  );
}
export default History;

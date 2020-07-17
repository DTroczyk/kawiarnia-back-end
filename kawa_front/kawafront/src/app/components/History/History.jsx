import React, { Fragment } from "react";
import "./History.scss";
import { useSelector } from "react-redux";
import Details from "../Details/Details";
import FadeIn from "react-fade-in";
import { Block, Wrapper, Field } from "./components";
function History() {
  const historyItems = useSelector((state) => state.history.historyItems);

  return (
    <Wrapper>
      {historyItems.map((order, idx) => {
        const { date, name, count, price, status, isCollapsed } = order;
        return (
          <Fragment key={idx}>
            <Field idx={idx}>
              <Block>{date}</Block>
              <Block>{name}</Block>
              <Block>{count}</Block>
              <Block>{price}</Block>
              <Block>{status}</Block>
            </Field>
            {isCollapsed && (
              <FadeIn>
                <Details idx={idx} />
              </FadeIn>
            )}
          </Fragment>
        );
      })}
    </Wrapper>
  );
}
export default History;

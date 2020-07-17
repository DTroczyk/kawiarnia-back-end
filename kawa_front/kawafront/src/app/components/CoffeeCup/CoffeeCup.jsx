import React from "react";
import "./CoffeeCup.scss";
import { useSelector } from "react-redux";
import FadeIn from "react-fade-in";
import ScrollButton from "../ScrollButton/ScrollButton";
import {
  Block,
  Button,
  Ear,
  Field,
  Fill,
  Properties,
  Input,
  Text,
  Wrapper,
} from "./components";
function CoffeeCup() {
  const orderProperties = useSelector((store) => store.order);

  return (
    <Wrapper>
      <Block>
        {Array.apply(null, {
          length: orderProperties.waterCount,
        }).map((e, i) => (
          <FadeIn key={i} delay="100">
            <Fill type="water" />
          </FadeIn>
        ))}
        {Array.apply(null, {
          length: orderProperties.milkCount,
        }).map((e, i) => (
          <FadeIn key={i} delay="100">
            <Fill type="milk" />
          </FadeIn>
        ))}
        {Array.apply(null, {
          length: orderProperties.espressoCount,
        }).map((e, i) => (
          <FadeIn key={i} delay="100">
            <Fill type="coffee" />
          </FadeIn>
        ))}
      </Block>
      <Ear />
      <Properties>
        <Field>
          <Text>Mleko</Text>
          {orderProperties.espressoCount + orderProperties.milkCount < 10 ? (
            <Button name="addMilk">+</Button>
          ) : null}

          {orderProperties.milkCount ? (
            <Button name="deleteMilk">-</Button>
          ) : null}
        </Field>
        <Field>
          <Text>Espresso</Text>
          {orderProperties.espressoCount + orderProperties.milkCount < 10 ? (
            <Button name="addEspresso">+</Button>
          ) : null}
          {orderProperties.espressoCount ? (
            <Button name="deleteEspresso">-</Button>
          ) : null}
        </Field>
        <Field>
          <Text>Czekolada</Text>
          <Input />
        </Field>
        <Field>
          <Text>{`Wartość Twojej kawy: ${new Intl.NumberFormat(
            window.navigator.language,
            {
              style: "currency",
              currency: "PLN",
            }
          ).format(orderProperties.price)}`}</Text>
        </Field>
        <ScrollButton goTo="map" />
      </Properties>
    </Wrapper>
  );
}
export default CoffeeCup;

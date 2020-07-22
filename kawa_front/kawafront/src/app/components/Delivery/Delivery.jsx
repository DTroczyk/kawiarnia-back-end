import React, { Fragment } from "react";
import "./Delivery.scss";
import { useState } from "react";
import FadeIn from "react-fade-in";
import { Block, Span, Wrapper, P } from "./components";

import Map from "../Map/Map";
import {
  Wrapper as AccountWrapper,
  Container,
  Field,
  Input,
  Span as AccountLabel,
  Label,
} from "../Account/components";
function Delivery() {
  const [selectedView, setSelectedView] = useState("menu");
  function getView() {
    switch (selectedView) {
      case "menu":
        return (
          <Fragment>
            <FadeIn>
              <Block
                id="choosedDefaultAdress"
                setSelectedView={setSelectedView}
              >
                <Span>Użyj</Span>
                <P>Domyślnego adresu z konta</P>
              </Block>
              <Block id="choosedNewAdress" setSelectedView={setSelectedView}>
                <Span>Użyj</Span>
                <P>nowego adresu konta</P>
              </Block>
              <Block id="choosedGeolocation" setSelectedView={setSelectedView}>
                <Span>Użyj geolokalizacji</Span>
                <P>bądź zaznacz na mapie</P>
              </Block>
            </FadeIn>
          </Fragment>
        );
      case "choosedDefaultAdress":
        return (
          <FadeIn>
            <AccountWrapper>
              <Container>
                <Field>
                  <Input name="houseNumber" />
                  <Span type="highlight" />
                  <Span type="bar" />
                  <Label>NUMER DOMU/LOKALU</Label>
                </Field>
                <Field>
                  <Input name="road" />
                  <Label>ULICA</Label>
                </Field>

                <Field>
                  <Input name="zipcode" />
                  <Span type="highlight" />
                  <Span type="bar" />
                  <Label>KOD POCZTOWY</Label>
                </Field>
                <Field>
                  <Input name="place" />
                  <Span type="highlight" />
                  <Span type="bar" />
                  <Label>MIEJSCOWOŚĆ</Label>
                </Field>
              </Container>
            </AccountWrapper>
          </FadeIn>
        );
      case "choosedNewAdress":
        return (
          <FadeIn>
            <AccountWrapper>
              <Container>
                <Field>
                  <Input name="houseNumber" />
                  <Span type="highlight" />
                  <Span type="bar" />
                  <Label>NUMER DOMU/LOKALU</Label>
                </Field>
                <Field>
                  <Input name="road" />
                  <Label>ULICA</Label>
                </Field>

                <Field>
                  <Input name="zipcode" />
                  <Span type="highlight" />
                  <Span type="bar" />
                  <Label>KOD POCZTOWY</Label>
                </Field>
                <Field>
                  <Input name="place" />
                  <Span type="highlight" />
                  <Span type="bar" />
                  <Label>MIEJSCOWOŚĆ</Label>
                </Field>
              </Container>
            </AccountWrapper>
          </FadeIn>
        );
      case "choosedGeolocation":
        return (
          <FadeIn>
            <Map />
          </FadeIn>
        );
      default:
        return <Fragment />;
    }
  }
  return (
    <Wrapper>
      {selectedView !== "menu" ? (
        <Block id="menu" setSelectedView={setSelectedView}>
          Powrót
        </Block>
      ) : null}
      {getView()}
    </Wrapper>
  );
}
export default Delivery;

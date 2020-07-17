import React from "react";
import "./DeleteItemFromBucket.scss";
import { Wrapper, Container, Field, Text } from "./components";
import { useSelector } from "react-redux";
function DeleteItemFromBucket() {
  const itemToDelete = useSelector((state) => state.bucket.itemToDelete);
  return (
    <Wrapper name={itemToDelete.coffeeName}>
      <Container>
        <Field>
          <Text>Nazwa kawy</Text>
          <Text>{itemToDelete.coffeeName}</Text>
        </Field>
        <Field>
          <Text>Cena </Text>
          <Text>{itemToDelete.price}</Text>
        </Field>
        <Field>
          <Text>Poziom mleka</Text>
          <Text>{itemToDelete.espressoCount}</Text>
        </Field>
      </Container>
    </Wrapper>
  );
}
export default DeleteItemFromBucket;

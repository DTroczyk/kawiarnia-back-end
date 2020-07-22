function elementScrollIntoView(name) {
  let element = Array.from(document.getElementsByClassName("newOrder__section"))
    .filter((el) => el.firstChild.className === name)
    .shift()
    .scrollIntoView({
      behavior: "smooth",
      block: "start",
      inline: "end",
    });
   return element 
}
export default elementScrollIntoView;

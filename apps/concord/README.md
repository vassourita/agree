> #### This folder will be deleted soon, as Concord now is a part of [Allow](../allow)

# Concord :tv:

Concord is the Agree main dashboard, built with CRA and Chakra UI.

I decided to divide it into three layers:
- The presentation layer, where is located most of the jsx and styling
- The logic layer, where I use React Contexts and Hooks to manage the entire application business rules
- The validation layer, where things get validated :P

### Dependency Injection

I think there is something very cool on how I handle DI in this project. Since I'm using hooks and contexts, there are no classes (and consequently no constructors for DI) so I inject my services into the props of the contexts providers on a "DI Container"-like component, but the contexts depend purely on interfaces so there is a very low coupling on the logic layer.
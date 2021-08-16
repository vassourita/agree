import { Button as ChakraButton, ButtonProps } from "@chakra-ui/react";
import { ReactNode } from "react";

type Props = {
  children: ReactNode
} & ButtonProps

export const Button = ({ children, ...rest }: Props) =>
  <ChakraButton
    h="3.5rem"
    w="full"
    bgColor="primary"
    textColor="text"
    mt="1.5rem"
    transitionProperty="filter"
    _hover={{ bgColor: "primary", filter: "brightness(0.9)" }}
    {...rest}
  >
    {children}
  </ChakraButton>
import { Input as ChakraInput, InputGroup, InputProps, InputRightElement, useDisclosure } from "@chakra-ui/react";
import { FiEye, FiEyeOff } from "react-icons/fi";

export const PasswordInput = ({ value, onChange, ...rest }: InputProps) => {
  const { isOpen, onToggle } = useDisclosure()

  return (
    <InputGroup {...rest}>
      <ChakraInput color="gray.300" value={value} onChange={onChange} h="3.5rem" borderColor="background2" bgColor="background2" type={!isOpen ? "password" : "text"} w="full" />
      <InputRightElement
        h="full"
        mr="8px"
        color="text2"
        fontSize="lg"
        onClick={onToggle}
      >
        {isOpen ? <FiEyeOff /> : <FiEye />}
      </InputRightElement>
    </InputGroup>
  )
}
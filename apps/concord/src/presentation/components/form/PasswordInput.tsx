import { useDisclosure } from '@chakra-ui/hooks'
import { InputLeftElement, Input, InputGroup, InputRightElement, InputProps } from '@chakra-ui/input'

import { ReactNode } from 'react'
import { FiEye, FiEyeOff } from 'react-icons/fi'

type PasswordInputProps = {
  icon: ReactNode
  placeholder: string
  w?: string | string[] | { base?: string, sm?: string, md?: string, lg?: string, xl?: string }
} & InputProps

export function PasswordInput ({ icon, placeholder, w = 'full', ...rest }: PasswordInputProps): JSX.Element {
  const { isOpen, onToggle } = useDisclosure()

  return (
    <InputGroup
      w={w} h="3.9rem"
      alignItems="center" justifyContent="center"
      border="1px" borderColor="gray.200"
      display="flex"
      bg="white"
      rounded="md"
      _focusWithin={{ borderColor: 'gray.300' }}
      _hover={{ borderColor: 'gray.300' }}
    >
      <InputLeftElement
        h="full"
        ml="12px"
        color="gray.600"
        fontSize="1.2em"
      >
        {icon}
      </InputLeftElement>
      <Input
        h="full"
        pl="55px" pb="3px"
        type={isOpen ? 'text' : 'password'}
        fontSize="1.05rem"
        color="gray.600"
        border="none"
        placeholder={placeholder}
        _placeholder={{ color: 'gray.600' }}
        {...rest}
      />
      <InputRightElement
        h="full"
        mr="8px"
        color="gray.600"
        fontSize="1.1em"
        onClick={onToggle}
      >
        {isOpen ? <FiEyeOff /> : <FiEye />}
      </InputRightElement>
    </InputGroup>
  )
}

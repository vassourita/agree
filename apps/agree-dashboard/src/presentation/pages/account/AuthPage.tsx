import { Box, Container, Flex, Grid, Heading, List, ListItem, Text } from '@chakra-ui/layout'
import { useHistory, useLocation } from 'react-router'
import { Link } from 'react-router-dom'
import { FiArrowLeftCircle, FiChevronRight, FiLock, FiMail, FiMenu, FiUser } from 'react-icons/fi'
import { TextInput } from '../../components/form/TextInput'
import { PasswordInput } from '../../components/form/PasswordInput'
import { SquareButton } from '../../components/form/SquareButton'
import { Slide } from '@chakra-ui/transition'
import { Button } from '../../components/form/Button'
import { useBreakpoints } from '../../hooks/useBreakpoints'
import { Menu, MenuButton, MenuDivider, MenuItem, MenuList } from '@chakra-ui/menu'
import { Image } from '@chakra-ui/image'

import LogoImage from '../../assets/agreew.svg'
import BgImage from '../../assets/bglogin.png'
import { useAuth } from '../../../logic/hooks/useAuth'
import { useInputState } from '../../hooks/useInputState'
import { FormEvent } from 'react'

export function AuthPage (): JSX.Element {
  const [loginEmail, setLoginEmail] = useInputState()
  const [loginPassword, setLoginPassword] = useInputState()

  const [registerEmail, setRegisterEmail] = useInputState()
  const [registerUserName, setRegisterUserName] = useInputState()
  const [registerPassword, setRegisterPassword] = useInputState()
  const [registerPasswordConfirm, setRegisterPasswordConfirm] = useInputState()

  const auth = useAuth()

  const location = useLocation()
  const history = useHistory()
  const [,, isMd] = useBreakpoints()

  const isRegisterPage = location.pathname === '/register'

  function submitLoginForm (e: FormEvent) {
    e.preventDefault()
    auth.login(loginEmail, loginPassword)
  }

  function submitRegisterForm (e: FormEvent) {
    e.preventDefault()
    auth.register(registerUserName, registerEmail, registerPassword)
    history.push('/login')
  }

  return (
    <>
      <Grid
        filter={isRegisterPage ? 'brightness(0.5)' : ''}
        templateAreas={{
          base: `
            'nav nav nav nav'
            'text text text text'
            'form form form form'
          `,
          lg: `
            '. nav nav .'
            'text text text text'
            'form form form form'
          `
        }}
        templateRows={{ base: 'auto 1fr auto', lg: '5rem 1fr 13rem' }}
        templateColumns="repeat(1fr, 4)"
        minH="100vh" w="full"
        bg={isMd ? 'brand.700' : `url(${BgImage})`}
      >
        <Flex gridArea="nav" as="nav">
          {isMd
            ? (
              <Flex direction="column" px="3rem">
                <Flex h="4rem" w="full" py="3rem" align="flex-start" justify="space-between">
                  <Image alt="Agree logo" h="3rem" src={LogoImage} mb="1.1rem" />
                  <Menu>
                    <MenuButton>
                      <FiMenu size={32} />
                    </MenuButton>
                    <MenuList shadow="md" bg="gray.100" color="gray.800">
                      <MenuItem bg="gray.100" color="gray.800">Página inicial</MenuItem>
                      <MenuDivider />
                      <MenuItem bg="gray.100" color="gray.800">Login</MenuItem>
                      <MenuItem onClick={() => history.push('/register')} bg="gray.100" color="gray.800">Criar uma conta</MenuItem>
                    </MenuList>
                  </Menu>
                </Flex>
                <Text maxW="430px" mt="1.7rem" fontSize="1.4rem" color="gray.100" lineHeight="1.4">
                  Faça login e conecte-se com outras 20 pessoas. Sim, só isso mesmo. Chama mais gente aí.
                </Text>
              </Flex>
              )
            : (
                <List fontSize="24" d="flex" mt="3rem" alignItems="end" justifyContent="center" w="full" textAlign="center" gridGap="4rem">
                  <ListItem><Link to="/">Página inicial</Link></ListItem>
                  <ListItem fontWeight={!isRegisterPage ? 'bold' : 'normal'}><Link to="/login">Login</Link></ListItem>
                  <ListItem fontWeight={isRegisterPage ? 'bold' : 'normal'}><Link to="/register">Criar uma conta</Link></ListItem>
                </List>
              )}
        </Flex>

        <Flex gridArea="text">
          <Container maxW="80rem">
            <Flex hidden={isMd} h="full" direction="column" justify="flex-end" align="start">
              <Image alt="Agree logo" h="100px" src={LogoImage} mb="1.1rem" />
              <Text maxW="430px" mb="4rem" fontSize="1.9rem" color="gray.100" lineHeight="1.4">
                Faça login e conecte-se com outras 20 pessoas. Sim, só isso mesmo. Chama mais gente aí.
              </Text>
            </Flex>
          </Container>
        </Flex>

        <Flex onSubmit={submitLoginForm} gridArea="form" as="form" bg="gray.100" align="center" justify="center" py={{ base: '3rem', lg: '0' }}>
          <Container maxW="80rem">
            <Flex direction={{ base: 'column-reverse', lg: 'row' }} justify="space-between" align="center">
              <Flex align="center" justify="center" mt={{ base: '2rem', lg: '0' }}>
                <Text fontSize="1.1rem" color="gray.700" lineHeight="1.4">
                  Ainda não tem uma conta?<br />
                  <Link style={{ textDecoration: 'underline' }} to="/register">Clique aqui</Link> para criar uma
                </Text>
              </Flex>
              <Flex direction={{ base: 'column', lg: 'row' }} gridGap={{ base: '1rem', lg: '50px' }}>
                <TextInput value={loginEmail} onChange={setLoginEmail} w={{ base: 'auto', lg: '300px' }} icon={<FiMail />} placeholder="EMAIL" />
                <PasswordInput value={loginPassword} onChange={setLoginPassword} w={{ base: 'auto', lg: '300px' }} icon={<FiLock />} placeholder="SENHA" />
                {isMd
                  ? (
                      <Button type="submit" h="3.9rem" rightIcon={<FiChevronRight />}>LOGIN</Button>
                    )
                  : (
                      <SquareButton type="submit" w="3.9rem" icon={<FiChevronRight size={20} />} />
                    )}
              </Flex>
            </Flex>
          </Container>
        </Flex>
      </Grid>

      <Slide style={{ zIndex: 10, right: 0, width: 'full', maxWidth: '500px' }} direction="right" in={isRegisterPage}>
        <Box overflowY="auto" shadow="md" maxW="500px" w="full" h="100vh" p="3rem" bg="gray.100" color="gray.800">
          <Flex w="auto" cursor="pointer" fontWeight="semibold" align="center" gridGap="1rem" onClick={() => history.push('/login')}>
            <FiArrowLeftCircle size={24} />
            VOLTAR
          </Flex>

          <Flex flexDir="column" marginTop="2.5rem">
            <Heading as="h2">Criar conta</Heading>
            <Text marginTop="0.5rem">
              Basta preencher esses dados para criar uma conta. Você poderá personalizar tudo isso depois.
            </Text>
          </Flex>

          <Flex onSubmit={submitRegisterForm} as="form" flexDir="column" marginTop="2.5rem" w="auto" gridRowGap="1.5rem">
            <TextInput value={registerUserName} onChange={setRegisterUserName} icon={<FiUser />} placeholder="NOME DE USUÁRIO" />
            <TextInput value={registerEmail} onChange={setRegisterEmail} icon={<FiMail />} placeholder="EMAIL" />
            <PasswordInput value={registerPassword} onChange={setRegisterPassword} icon={<FiLock />} placeholder="DIGITE SUA SENHA" />
            <PasswordInput value={registerPasswordConfirm} onChange={setRegisterPasswordConfirm} icon={<FiLock />} placeholder="CONFIRME SUA SENHA" />
            <Button type="submit" h="3.9rem" rightIcon={<FiChevronRight />}>CRIAR CONTA</Button>
          </Flex>
        </Box>
      </Slide>
    </>
  )
}

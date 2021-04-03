import { Container, Flex, Grid, List, ListItem, Spacer, Text } from '@chakra-ui/layout'
import { useLocation } from 'react-router'
import { Link } from 'react-router-dom'
import { FiChevronRight, FiLock, FiMail } from 'react-icons/fi'
import { TextInput } from '../../components/form/TextInput'
import { PasswordInput } from '../../components/form/PasswordInput'
import { SquareButton } from '../../components/form/SquareButton'

export function LoginPage (): JSX.Element {
  const location = useLocation()

  return (
    <Grid
      templateAreas="
        '. nav nav .'
        'text text text text'
        'form form form form'
      "
      templateRows="5rem 1fr 13rem"
      templateColumns="1fr 1fr 1fr 1fr"
      h="100vh" w="full"
      bg="brand.700"
    >
      <Flex gridArea="nav" as="nav">
        <List fontSize="24" d="flex" mt="3rem" alignItems="end" justifyContent="center" w="full" textAlign="center" gridGap="4rem">
          <ListItem><Link to="/">Página inicial</Link></ListItem>
          <ListItem fontWeight={location.pathname === '/login' ? 'bold' : 'normal'}><Link to="/login">Login</Link></ListItem>
          <ListItem fontWeight={location.pathname === '/register' ? 'bold' : 'normal'}><Link to="/register">Criar uma conta</Link></ListItem>
        </List>
      </Flex>

      <Flex gridArea="text">
        <Container maxW="80rem">
          <Text fontSize="1.1rem" color="gray.100" lineHeight="1.4">
            Logo
          </Text>
        </Container>
      </Flex>

      <Flex gridArea="form" as="form" bg="gray.100" align="center" justify="center">
        <Container maxW="80rem">
          <Flex>
            <Flex align="center" justify="center">
              <Text fontSize="1.1rem" color="gray.700" lineHeight="1.4">
                Ainda não tem uma conta?<br />
                <Link style={{ textDecoration: 'underline' }} to="/register">Clique aqui</Link> para criar uma
              </Text>
            </Flex>
            <Spacer />
            <Flex gridGap="50px">
              <TextInput icon={<FiMail />} placeholder="EMAIL" />
              <PasswordInput icon={<FiLock />} placeholder="SENHA" />
              <SquareButton w="3.9rem" icon={<FiChevronRight size={20} />} />
            </Flex>
          </Flex>
        </Container>
      </Flex>
    </Grid>
  )
}

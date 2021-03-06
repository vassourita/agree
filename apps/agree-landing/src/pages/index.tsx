import { motion } from 'framer-motion'
import { FiDollarSign, FiDownload, FiGithub, FiGlobe, FiHelpCircle, FiSmartphone } from 'react-icons/fi'
import { useMedia } from 'react-use'
import { AsideLink } from '../components/Home/AsideLink'
import { NavLink } from '../components/Home/NavLink'

export default function Home () {
  const isSmallScreen = useMedia('(max-width: 767px)')

  return (
    <main className="px-6 md:px-20">
      <header className="container mt-7 md:mt-10 mx-auto flex items-center justify-between">
        <motion.img whileHover={{ scale: 1.03 }} src="/agreew.svg" className="h-12 md:h-16 cursor-pointer" />
        <nav>
          <ul className="flex text-text-dark gap-10">
            <NavLink icon={FiHelpCircle} iconSize={isSmallScreen ? 20 : 24} />
            <NavLink icon={FiGithub} iconSize={isSmallScreen ? 20 : 24} />
            <NavLink icon={FiDollarSign} iconSize={isSmallScreen ? 20 : 24} />
          </ul>
        </nav>
      </header>

      <div className="container mx-auto mt-16 flex flex-col lg:flex-row justify-between">
        <aside className="mb-8 lg:mb-0 lg:w-44">
          <ul className="flex lg:flex-col gap-x-5 sm:gap-x-7 md:gap-x-9 lg:gap-y-12">
            <AsideLink disabled={false} icon={FiGlobe} iconSize={isSmallScreen ? 19 : 20} href="/login">
              Abrir no <br/>
              navegador
            </AsideLink>
            <AsideLink disabled={true} icon={FiDownload} iconSize={isSmallScreen ? 19 : 20} href="/download">
              Download <br/>
              para Linux
            </AsideLink>
            <AsideLink disabled={true} icon={FiSmartphone} iconSize={isSmallScreen ? 19 : 20} href="/mobile">
              Download <br/>
              para celular
            </AsideLink>
          </ul>
        </aside>

        <div className="flex items-center lg:justify-center">
          <h1 className="lg:text-right text-text-dark text-5xl md:text-6xl lg:text-7xl font-bold">
            Isso aqui <br/>
            definitivamente, <br/>
            absolutamente e <br/>
            indiscutivelmente <br/>
            (não) é um clone do <br/>
            Discord.
          </h1>
        </div>
      </div>
    </main>
  )
}

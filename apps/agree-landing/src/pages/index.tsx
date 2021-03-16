import { motion } from 'framer-motion'
import { FiDollarSign, FiDownload, FiGithub, FiGlobe, FiHelpCircle, FiSmartphone } from 'react-icons/fi'
import { useMedia } from 'react-use'
import { AsideLink } from '../components/home/AsideLink'
import { NavLink } from '../components/home/NavLink'

export default function Home () {
  const isSmallScreen = useMedia('(max-width: 767px)')
  const isExtraSmallScreen = useMedia('(max-width: 639px)')

  return (
    <main className="px-4 md:px-20 min-h-screen h-full bg-fixed bg-cover bg-center transition-all overflow-hidden flex flex-col justify-between" style={{ backgroundImage: 'url(./assets/images/bg_home.png)' }}>
      <header className="container pt-7 md:pt-16 mx-auto flex items-center justify-between">
        <motion.img whileHover={{ scale: 1.03 }} src="/agreew.svg" className="h-12 md:h-16 xl:h-20 cursor-pointer" />
        <nav>
          <ul className="flex text-text-dark gap-7 md:gap-10">
            <NavLink icon={FiHelpCircle} iconSize={isExtraSmallScreen || isSmallScreen ? 21 : 25} />
            <NavLink icon={FiGithub} iconSize={isExtraSmallScreen || isSmallScreen ? 21 : 25} />
            <NavLink icon={FiDollarSign} iconSize={isExtraSmallScreen || isSmallScreen ? 21 : 25} />
          </ul>
        </nav>
      </header>

      <div className="h-full container mx-auto lg:mt-16 flex flex-col lg:flex-row justify-between lg:items-center">
        <aside className="lg:w-44 xl:w-52">
          <ul className="grid grid-cols-3 grid-rows-1 lg:grid-cols-1 lg:grid-rows-1 gap-x-4 sm:gap-x-8 md:gap-x-9 lg:gap-y-12">
            <AsideLink disabled={false} icon={FiGlobe} iconSize={isSmallScreen ? 19 : 20} href="/login">
              Abrir no <br/>
              navegador
            </AsideLink>
            <AsideLink disabled={true} icon={FiDownload} iconSize={isSmallScreen ? 19 : 20} href="/download">
              Download <br/>
              {!isExtraSmallScreen && 'para'} Windows
            </AsideLink>
            <AsideLink disabled={true} icon={FiSmartphone} iconSize={isSmallScreen ? 19 : 20} href="/mobile">
              Download <br/>
              {!isExtraSmallScreen && 'para'} celular
            </AsideLink>
          </ul>
        </aside>

        <div className="flex items-center lg:justify-end h-full py-6">
          <h1 className="lg:text-right text-text-dark leading-none text-4.5xl sm:text-6xl lg:text-7xl xl:text-8xl font-bold">
            Isso aqui <br/>
            definitivamente, <br/>
            absolutamente e <br/>
            indiscutivelmente <br/>
            (não) é um clone <br/>
            do Discord.
          </h1>
        </div>
      </div>
    </main>
  )
}

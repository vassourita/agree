import Image from 'next/image'
import { FiChevronDown, FiDollarSign, FiDownload, FiGithub, FiGlobe, FiHelpCircle, FiSmartphone } from 'react-icons/fi'

export default function Home () {
  return (
    <div className="h-screen w-full">
      <header className="bg-primary h-full">

        <div className="container h-full m-auto flex flex-col justify-between">

          <nav className="w-full pt-11 flex justify-between items-center">
            <div className="h-20 cursor-pointer">
              <Image src="/agreew.svg" alt="Agree logo" width="auto" height="68px" />
            </div>

            <div className="flex text-text-dark gap-10">
              <FiHelpCircle className="cursor-pointer" size="30" />
              <FiGithub className="cursor-pointer" size="30" />
              <FiDollarSign className="cursor-pointer" size="30" />
            </div>
          </nav>

          <div className="h-full flex gap-x-10 items-end mb-10">
            <div className="flex flex-col items-center">
              <span className="text-text-dark font-bold">Em breve</span>
              <button
                className="transform opacity-80 cursor-not-allowed transition-all w-64 bg-button flex items-center justify-center p-4 text-lg text-primary font-bold rounded-md border-none outline-none focus:outline-none"
                >
                Abrir no navegador <FiGlobe className="ml-3" />
              </button>
            </div>

            <div className="flex flex-col items-center">
              <span className="text-text-dark font-bold">Em breve</span>
              <button
                disabled
                className="transform opacity-80 cursor-not-allowed transition-all w-64 bg-button flex items-center justify-center p-4 text-lg text-primary font-bold rounded-md border-none outline-none focus:outline-none"
              >
                Download <FiDownload className="ml-3" />
              </button>
            </div>

            <div className="flex flex-col items-center">
              <span className="text-text-dark font-bold">Em breve</span>
              <button
                disabled
                className="transform opacity-80 cursor-not-allowed transition-all w-64 bg-button flex items-center justify-center p-4 text-lg text-primary font-bold rounded-md border-none outline-none focus:outline-none"
              >
                Mobile <FiSmartphone className="ml-3" />
              </button>
            </div>
          </div>

          <h1 className="text-text-dark font-bold text-7xl pb-24">
            Te garanto que isso aqui<br/>
            definitivamente,<br/>
            absolutamente e<br/>
            indiscutivelmente<br/>
            (não) é uma cópia do Discord.
          </h1>

          <div className="pb-4 flex items-center justify-center text-text-dark">
            <FiChevronDown size="24" />
          </div>

        </div>

      </header>

      <main className="container min-h-screen">
        <section>
          <h3></h3>
          <p></p>
        </section>

        <section>
          <h3></h3>
          <p></p>
        </section>

        <section>
          <h3></h3>
          <p></p>
        </section>
      </main>

      <footer className="container"></footer>
    </div>
  )
}

export const Footer = () => {
  return (
    <>
      <footer className="footer footer-center p-4 bg-base-300 flex">
        {/* TODO: Add icons links (all) */}
        <p className="flex">
          Icons by
          <a
            href="https://icons8.com"
            title="icons8"
            className="link link-info"
          >
            Icons8
          </a>
        </p>

        <a
          href="https://www.flaticon.com/free-icons/cpu-tower"
          title="cpu tower icons"
        >
          Cpu tower icons created by Good Ware - Flaticon
        </a>
        <a
          href="https://www.flaticon.com/free-icons/computer-case"
          title="computer case icons"
        >
          Computer case icons created by Erifqi Zetiawan - Flaticon
        </a>
        <a
          href="https://www.flaticon.com/free-icons/cooling"
          title="cooling icons"
        >
          Cooling icons created by Smashicons - Flaticon
        </a>
      </footer>
    </>
  );
};

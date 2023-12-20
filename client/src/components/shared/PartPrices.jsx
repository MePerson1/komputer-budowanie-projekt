const PartPrices = ({ price }) => {
  return (
    <div class="dropdown dropdown-right">
      <div tabindex="0" role="button" class="btn m-1">
        {price} zł
      </div>
      <ul
        tabindex="0"
        class="dropdown-content z-[1] menu p-2 shadow bg-base-100 rounded-box w-52"
      >
        <li>
          <p>
            <a href="https://www.google.com/">Cena1 - Sklep1</a>
          </p>
        </li>
        <li>
          <p>
            Cena2 - <a>Sklep2</a>
          </p>
        </li>
      </ul>
    </div>
  );
};
export default PartPrices;

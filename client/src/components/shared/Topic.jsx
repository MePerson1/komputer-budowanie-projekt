const Topic = ({ title, autor }) => {
  return (
    <div className="bg-base-200 p-5">
      <h1 className="text-4xl">{title}</h1>
      {autor && (
        <h2 className="mt-5 ml-5 text-[1.5rem] text-white text-opacity-60">
          Twórca: {autor}
        </h2>
      )}
    </div>
  );
};

export default Topic;

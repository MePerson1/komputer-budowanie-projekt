export const TextInput = ({ name }) => {
  return (
    <div>
      <label class="label">
        <span class="text-base label-text">{name}</span>
      </label>
      <input
        type="text"
        name={name}
        class="w-full input input-bordered input-primary"
        required
      />
    </div>
  );
};

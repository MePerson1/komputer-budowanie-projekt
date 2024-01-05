export const TextInput = ({
  type,
  title,
  name,
  value,
  onChange,
  placeholder,
}) => {
  return (
    <div>
      <label class="label">
        <span class="text-base label-text">{title}</span>
      </label>
      <input
        type={type}
        name={name}
        placeholder={placeholder}
        value={value}
        onChange={onChange}
        class="w-full input input-bordered input-primary"
        autoComplete="off"
        required
      />
    </div>
  );
};

import React, { FC } from 'react';

interface GuardianProps {
  name: string;
}

const Guardian: FC<GuardianProps> = ({ name }) => {
  return (
    <div>
      <p>{name}</p>
    </div>
  );
};

export default Guardian;
import { User } from 'models';
import { useCallback, useEffect, useState } from 'react';

const defaultUser: User = {
  id: '',
  authenticated: false,
  email: '',
  firstName: '',
  lastName: '',
  roles: [],
};

export const useUser: () => [User, () => void] = () => {
  const [user, setUser] = useState<User>(defaultUser);
  const getUser = async () => {
    try {
      const result = await fetch(`${process.env.NEXT_PUBLIC_HOST}/user`);
      if (result.ok) {
        const userData = (await result.json()) as User;
        userData.authenticated = true;
        setUser(userData);
      }
    } catch (err) {}
  };
  useEffect(() => {
    getUser();
  }, []);

  const logout = useCallback(() => setUser(defaultUser), []);
  return [user, logout];
};

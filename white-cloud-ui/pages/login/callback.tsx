import { useEffect } from 'react';

const Callback = () => {
  useEffect(() => {
    const code = (location.search.match(/code=([^&]+)/) || [])[1];
    const state = (location.search.match(/state=([^&]+)/) || [])[1];
    const qParams = [`code=${code}`];
    fetch(`${process.env.NEXT_PUBLIC_HOST}/authentication/oidc/${state}?${qParams}`);
  }, []);

  return <p></p>;
};

export default Callback;

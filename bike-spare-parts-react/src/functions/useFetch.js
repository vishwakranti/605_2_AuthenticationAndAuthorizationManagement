import { useEffect, useState } from "react";
import axios from "axios";

const useFetch = (url) => {
  const [data, setData] = useState(null);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(null);

  useEffect(() => {
    (async function () {
      try {
        setLoading(true);
        const response = await axios.get(url);
        //debugger;
        setData(response.data); // have data to set data
      } catch (err) {
        setError(err); // otherwise set error
      } finally {
        setLoading(false); // set loading to false
      }
    })();
  }, [url]); // the only dependency we are going to put in the useeffect dependency array is Url because if the Url changes ,we have to request new data
  console.log(data);
  return { error, data, loading };
};

export default useFetch;

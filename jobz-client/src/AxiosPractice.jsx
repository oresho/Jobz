import axios from 'axios';
import React, { useEffect, useState } from 'react'

const client = axios.create({
    baseURL: "https://jsonplaceholder.typicode.com/posts" 
  });

function AxiosPractice() {
    const [post, setPost] = useState(null);

    useEffect(() => {
        async function getPost() {
            const response = await client.get("/1");
            setPost(response.data);
            console.log(response.data);
        }
        getPost();
    }, []);

    async function createPost() {
        const response = await client.post("",
        {
            title: "Hello World!",
            body: "This is a new post."
        });
        setPost(response.data);
        console.log(response.data);
      }

  return (
    <div className='flex flex-col mb-10'>AddJobsPage
        <button onClick={createPost}>Create Post</button>
        </div>
  )
}

export default AxiosPractice
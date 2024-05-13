const client = axios.create({
    baseURL: "https://localhost:7058/api/v1/jobs" 
})

function jobService() {
  return (
    <div>jobService</div>
  )
}
async function addJob(job){
    const response = await client.post("",
    {
        job
    });
    return response;
}

async function deleteJob(jobId){
    const response = await client.delete(`/${jobId}`);
    return response;
}

async function editJob(jobId, updatedJob){
    const response = await client.put(`/${jobId}`,
        {
            updatedJob
        }
    );
    return response;
}

export default jobService
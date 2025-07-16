import Guardian from "../Components/Guardian"

export default async function Page() {
  type guardianResponse = {firstName: string, lastName: string}
  const data = await fetch('http://localhost:5078/guardians')
  const guardians = (await data.json()) as guardianResponse[]
  return (
    <div>
      <h1>Guardians</h1>
      {guardians.map((g, i) => <Guardian key={i} name={`${g.firstName} ${g.lastName}`}/>)}
    </div>
  )
}
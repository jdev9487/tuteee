import Link from "next/link"
import { guardianResponse } from "../types/GuardianResponse"

export default async function Page() {
  const data = await fetch('http://localhost:5078/guardians')
  const guardians = (await data.json()) as guardianResponse[]
  return (
    <div>
      <h1>Guardians</h1>
      {guardians.map((g, i) => <Link key={i} href={`/guardians/${g.guardianId}`}><p>{`${g.firstName} ${g.lastName}`}</p></Link>)}
    </div>
  )
}
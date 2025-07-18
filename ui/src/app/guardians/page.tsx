import Link from "next/link"
import { guardianResponse } from "../types/GuardianResponse"

export const dynamic = "force-dynamic"

export default async function Page() {
  const data = await fetch(`${process.env.NEXT_PUBLIC_API}/guardians`, { cache: 'no-store' })
  const guardians = (await data.json()) as guardianResponse[]
  return (
    <div>
      <h1>Guardians</h1>
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Children</th>
          </tr>
        </thead>
        <tbody>
          {guardians.map((g, i) => 
            <tr key={i}>
              <td><Link key={i} href={`/guardians/${g.guardianId}`}><p>{`${g.firstName} ${g.lastName}`}</p></Link></td>
              <td>{g.tutees.map((t, j) => <p key={j}><Link href={`/tutees/${t.tuteeId}`}>{`${t.firstName} ${t.lastName}`}</Link></p>)}</td>
            </tr>
          )}
        </tbody>
      </table> 
    </div>
  )
}
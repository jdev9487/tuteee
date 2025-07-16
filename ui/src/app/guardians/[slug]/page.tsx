import Link from "next/link"
import { guardianResponse } from "../../types/GuardianResponse"

export default async function Page({
  params,
}: {
  params: Promise<{ slug: number }>
}) {
  const { slug } = await params
  const data = await fetch(`http://localhost:5078/guardians/${slug}`)
  const guardian = (await data.json()) as guardianResponse

  function getChildren(){
    if (guardian.tutees.length !== 0)
    {
      return (
        <div>
          <h2>Children</h2>
          {guardian.tutees.map((t, i) => <Link href={`/tutees/${t.tuteeId}`} key={i}><p>{t.firstName} {t.lastName}</p></Link>)}
        </div>
      )
    }
  }

  return (
    <div>
      <h1>{guardian.firstName} {guardian.lastName}</h1>
      {getChildren()}
    </div>
  )
}
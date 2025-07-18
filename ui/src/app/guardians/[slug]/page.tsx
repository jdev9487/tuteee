import Link from "next/link"
import { guardianResponse } from "../../types/GuardianResponse"

export default async function Page({
  params,
}: {
  params: Promise<{ slug: number }>
}) {
  const { slug } = await params
  const data = await fetch(`${process.env.NEXT_PUBLIC_API}/guardians/${slug}`, { cache: 'no-store' })
  const guardian = (await data.json()) as guardianResponse

  function getChildren(){
    if (guardian.tutees.length !== 0)
    {
      return (
        <div>
          {guardian.tutees.map((t, i) => <p key={i}><Link href={`/tutees/${t.tuteeId}`}>{t.firstName} {t.lastName}</Link> (Tutee)</p>)}
        </div>
      )
    }
  }

  return (
    <div>
      <h1>{guardian.firstName} {guardian.lastName} (Guardian)</h1>
      {getChildren()}
    </div>
  )
}

import React from 'react'
import { useEffect } from 'react'
import {useDispatch} from 'react-redux' 
import {kURL } from '../helpers/consts'
import bucketActions from '../redux/bucket/actions',
import {getFetchHeader }from '../helpers'
import useToken from '../hooks/useToken'
import { useState } from 'react'
export default ()=>{
    const dispatch =useDispatch()
    const token = useToken()
    const [bucketItems,setBucketItems] = useState([])
    useEffect(()=>{
        fetch(`${kURL}/api/bucket`,getFetchHeader('GET',token))
        .then(res=>res.json())
        .then(json=>{setBucketItems(json)})
        .catch(err=>console.log(err))

    },[bucketItems])
    return bucketItems;

}
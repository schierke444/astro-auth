import{c as e}from"./QueryProviderHoc.92946f15.js";const r=async()=>(await e.get("/Items")).data,n=async t=>await e.post("/Items",t),c=async t=>await e.delete(`/Items/${t}`);export{n as a,c as d,r as g};

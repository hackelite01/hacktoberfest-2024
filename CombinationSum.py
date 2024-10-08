class Solution:
    def combinationSum(self, candidates: List[int], target: int) -> List[List[int]]:
        res=[]
        def dfs(candidates,target,path,res):
            if target==0:
                res.append(path)
                return
            for i in range(len(candidates)):
                if candidates[i]>target:
                    continue
                dfs(candidates[i:],target-candidates[i],path+[candidates[i]],res)
        dfs(candidates,target,[],res)
        return res

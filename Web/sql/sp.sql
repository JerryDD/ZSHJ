USE [basedb_fwq]
GO
/****** Object:  StoredProcedure [dbo].[prUpdate_SDSMFWQ_KHSM]    Script Date: 04/24/2015 18:30:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[prWeb_Update_SDSMFWQ_KHSM]
@bgh VARCHAR(50),
@khqrsm VARCHAR(200),
@chvMsg VARCHAR(50) OUTPUT
AS
BEGIN
	SET @chvMsg =''
	UPDATE SDSM_FWQ SET 客户确认说明 = @khqrsm WHERE 报告号 = @bgh
	IF @@ROWCOUNT = 0
	BEGIN
		SET @chvMsg = '报告不存在'
		RETURN -1
	END	
    RETURN 1
END
GO

CREATE PROCEDURE [dbo].[prWeb_SELECT_ONE_SDSM_FWQ]
	@bgh VARCHAR(50)
AS
BEGIN
	SELECT top 1
	收集资料,数据采集时间,数据采集说明,
	录入信息,信息录入时间,录入信息说明,
	报告处理,报告处理时间,报告处理说明,
	报告审核,报告审核时间,报告审核说明,
	--确认税款,
	客户确认,客户确认时间,客户确认说明,
	报告上传,报告上传时间,报告上传说明,
	报告打印,报告打印时间,报告打印说明,
	--等待出库,
	报告报盘时间,报告报盘说明,
	报告出库,报告出库时间,报告出库说明--,已完成
	FROM dbo.SDSM_FWQ WHERE 报告号=@bgh
END
GO

CREATE PROCEDURE [dbo].[prWeb_SELECTALL_SDSM_FWQ]
	@khbm VARCHAR(50),
	@gsmc VARCHAR(200),
	@chvOrderByColumn VARCHAR(50),
	@chvOrderDirection VARCHAR(10),
	@chvStatus VARCHAR(50)
AS
BEGIN	
	DECLARE @sql VARCHAR(2000)
			,@where VARCHAR(500)
			,@orderby VARCHAR(100)
	SET @sql = 'SELECT 公司名称, 报告号,客户确认说明,应纳税额28,减免所得税额26,已缴税32,应补税33,当前问题说明,当前状态 
	FROM dbo.SDSM_FWQ WHERE 客户编码='''+@khbm+''''
	SET @where = ''
	IF LEN(@gsmc)>0
		SET @where = @where + ' AND 公司名称 LIKE ''%'+@gsmc+'%'''
	--数据采集和等待出库在页面上有，但是数据库中没有这个状态，数据库中有报告出库状态，不知道和什么对应
	IF @chvStatus = '数据采集' OR @chvStatus = '等待出库'
		SET @where = @where + ''
	ELSE IF @chvStatus = @where + '录入信息'
		SET @where =  @where + 'AND 当前状态 = ''录入信息'''
	ELSE IF @chvStatus = '报告处理'
		SET @where = @where + 'AND 当前状态 = ''报告处理'''
	ELSE IF @chvStatus = '报告审核'
		SET @where = @where + ' AND 当前状态 = ''报告审核'''
	ELSE IF @chvStatus = '确认税款' --这里有问题
		SET @where = @where + ' AND 当前状态 = ''确认税款'''
	ELSE IF @chvStatus = '报告上传'
		SET @where = @where + ' AND 当前状态 = ''报告上传'''
	ELSE IF @chvStatus = '报告打印'
		SET @where = @where + ' AND 当前状态 = ''报告打印'''	
	ELSE IF @chvStatus = '已完成'
		SET @where = @where + ' AND 当前状态 = ''已完成'''							
	
	IF @chvOrderByColumn = ''
		SET @orderby = ' ORDER BY 数据采集时间 DESC,信息录入时间 DESC'
	ELSE
		SET @orderby = ' ORDER BY '+ @chvOrderByColumn +' ' + 	@chvOrderDirection
	
	SET @sql = @sql + @where + @orderby		
	select @sql as sqld
	--EXEC(@sql)	
	RETURN 1 
END
GO
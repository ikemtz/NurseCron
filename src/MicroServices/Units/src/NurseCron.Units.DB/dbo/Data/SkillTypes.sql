IF NOT EXISTS (SELECT 1 FROM [SkillTypes] WITH (NOLOCK))
BEGIN
	INSERT [SkillTypes] ([Id], [Name]) VALUES (1, N'Certifications')
	INSERT [SkillTypes] ([Id], [Name]) VALUES (2, N'Competencies')
	INSERT [SkillTypes] ([Id], [Name]) VALUES (4, N'HealthItems')
END
